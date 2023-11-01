<template>
    <CDropDown v-model="dataModel" :filter="filter" :useDefaultFilter="false" :options="optionsData" optionLabel="name" :placeholder="placeholder" :showClear="true" 
              @show="onOpen" @hide="onClose" @filter="delayFilter" @change="onChange" :hasNext="hasNextPage" :loading="loading"  >
        <template #value="slotProps">
            <div v-if="slotProps.value">
                <div>{{ slotProps.value.text }}</div>
            </div>
            <span v-else>
                {{slotProps.placeholder}}
            </span>
        </template>
        <template #option="slotProps">
            <div>{{ show(slotProps) }}</div>
        </template>
        <template #loadMore>
            <li v-show="hasNextPage" class="p-dropdown-empty-message" ref="load">
                <span name="more">Load more...</span>
            </li>
        </template>
    </CDropDown>
</template>
<script>
    import DropDown from './Dropdown.vue'

    export default {
        name: "CustomSelect",
        emits: [
            "change", "init", "init-data"
        ],
        props: {
            modelValue: null,
            minLength: {
                type: Number,
                default: 3
            },
            url: String,
            params: Object,
            placeholder: {
                type: String,
                default: "Please select"
            },
            initFirst:{
                type:Boolean,
                default:false
            },
            filter:{
                type:Boolean,
                default:false
            },
            delay:{
                type:Number,
                default:0 
            }
        },
        data() {
            return {
                dataModel: this.modelValue,
                optionsData: [],
                observer: null,
                page: 0,
                searchFilter: "",
                hasNextPage: false,
                loading: true,
                timer:0,
            }
        },
        watch: {
            hasNextPage(value) {
                if (value) {
                    if (this.hasNextPage) {
                        this.$nextTick(function () {
                            this.observer.observe(this.$refs.load);
                        });

                    }
                }
            },
            modelValue(value){
                this.dataModel = value;
                this.onInit();
               
            }
        },
        mounted() {
            this.observer = new IntersectionObserver(this.infiniteScroll);
            this.onInit();
        },
        methods: {
            async onOpen() {
                this.page = 0;
                this.hasNextPage = false;
                this.searchFilter = "";
                this.optionsData = [];

                if (this.hasNextPage) {
                    await this.$nextTick();
                    this.observer.observe(this.$refs.load);
                }

                if (this.minLength == 0) {
                    this.filterData("");
                }
            },
            onClose() {
                this.observer.disconnect();
            },
            async infiniteScroll([{ isIntersecting, target }]) {
                if (isIntersecting) {
                    const ul = target.offsetParent;
                    const scrollTop = target.offsetParent.scrollTop;
                    this.page += 1;
                    await this.$nextTick();
                    ul.scrollTop = scrollTop;

                    this.filterData(this.searchFilter, true);
                }
            },
            delayFilter(event){
                if(this.delay == 0){
                    this.onFilter(event);
                }
                else{
                    var self = this;
                    clearTimeout(this.timer);

                    this.timer = setTimeout(function(){
                        self.onFilter(event);

                        console.log("HERE");
                    }.bind(this), this.delay);
                }
            },
            onFilter(event) {
                this.searchFilter = event.value;
                this.page = 0;
                

                this.filterData(this.searchFilter,false);
            },
            filterData(filter, more) {
                var filterParams = {
                    q: filter,
                    page: more == true ? this.page : 0
                };

                var fParams = filterParams;
                if (this.params != null) {
                    Object.assign(fParams, this.params);
                }

                this.loading = true;
                this.optionsData = [];
                var self = this;

                this.$axios.get(this.url, { params: fParams }).then((response) => {
                    if (more) {
                        var appended = this.optionsData.concat(response.data.items);
                        self.optionsData = appended;
                        //this.optionsData.push(response.data.items);
                    }
                    else {
                        self.optionsData = response.data.items;
                    }

                    self.loading = false;
                    self.hasNextPage = response.data.items && response.data.items.length >= 10;
                })
                .catch(() => {
                    self.loading = false;
                });
            },
            show(data) {
                return data.option.text;
            },
            onChange(event) {
                var value = event.value == null ? null : event.value.id;
                this.$emit("update:modelValue", value);
                this.$emit("change", event.value);
            },
            onInit() {
                var fParams;
                var filterParams;

                this.$emit("init");

                if(this.dataModel && this.dataModel != 0 && this.dataModel != ""){
                    filterParams = {
                        q: this.dataModel,
                        page: 0,
                        init: true
                    };

                    fParams = filterParams;
                    if (this.params != null) {
                        Object.assign(fParams, this.params);
                    }

                    var self =this;

                    this.$axios.get(this.url, { params: fParams }).then((response) => {
                        if (response.data.items && response.data.items.length > 0) {
                            this.dataModel = response.data.items[0];

                            self.$emit("init-data", response.data.items[0]);
                        }
                    });
                }
                else{
                    if(this.initFirst){
                        filterParams = {
                            q: "",
                            page: 0,
                            init: false
                        };

                        fParams = filterParams;
                        if (this.params != null) {
                            Object.assign(fParams, this.params);
                        }

                        this.$axios.get(this.url, { params: fParams }).then((response) => {
                            if (response.data.items && response.data.items.length > 0) {
                                this.dataModel = response.data.items[0];

                            }
                        });
                    }
                   
                }
            }

        },
        components: {
            'CDropDown': DropDown
        }
    }
</script>

