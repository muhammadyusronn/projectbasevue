<template>
  <Teleport to="body">
    <transition
      name="p-sidebar"
      @enter="onEnter"
      @leave="onLeave"
      @after-leave="onAfterLeave"
      appear
    >
      <div
        :class="containerClass"
        v-if="visible"
        ref="container"
        role="complementary"
        :aria-modal="modal"
        v-bind="$attrs"
      >
        <BlockUI :blocked="blocked">
          <Toolbar>
            <template #left>
              <b
                ><h4 class="m-0">{{ title }}</h4></b
              >
            </template>
            <template #right>
              <slot name="actions"></slot>
              &nbsp;
              <Button
                icon="pi pi-times"
                @click="hide"
                :aria-label="ariaCloseLabel"
                v-if="showCloseIcon"
                :label="exitLabel"
                class="p-button p-button-plain"
              ></Button>
              <!--<button class="p-sidebar-close p-link" @click="hide" :aria-label="ariaCloseLabel" v-if="showCloseIcon" type="button" v-ripple>
                                <span class="p-sidebar-close-icon pi pi-times" />
                            </button> -->
            </template>
          </Toolbar>

          <div class="p-sidebar-content mt-1">
            <slot></slot>
          </div>
        </BlockUI>
      </div>
    </transition>
  </Teleport>
</template>

<script>
import { DomHandler, ZIndexUtils } from "primevue/utils";
import Ripple from "primevue/ripple";

export default {
  emits: ["update:visible", "show", "hide"],
  inheritAttrs: false,
  props: {
    exitLabel: {
      type: String,
      default: "Cancel",
    },
    title: {
      type: String,
      default: "",
    },
    visible: {
      type: Boolean,
      default: false,
    },
    position: {
      type: String,
      default: "left",
    },
    baseZIndex: {
      type: Number,
      default: 0,
    },
    autoZIndex: {
      type: Boolean,
      default: true,
    },
    dismissable: {
      type: Boolean,
      default: true,
    },
    showCloseIcon: {
      type: Boolean,
      default: true,
    },
    modal: {
      type: Boolean,
      default: true,
    },
    ariaCloseLabel: {
      type: String,
      default: "close",
    },
    blocked: {
      type: Boolean,
      default: false,
    },
  },
  mask: null,
  maskClickListener: null,
  container: null,
  beforeMount() {
        window.addEventListener('keyup', this.onEscapeKeyUp);
  },
  beforeUnmount() {
    this.destroyModal();

    if (this.container && this.autoZIndex) {
      ZIndexUtils.clear(this.container);
    }
    this.container = null;

    window.addEventListener('keyup', this.onEscapeKeyUp);
  },
  methods: {
    onEscapeKeyUp(event){
      if (event.which === 27) {
        this.$emit("update:visible", false);
      }
    },
    hide() {
      this.$emit("update:visible", false);
    },
    onEnter(el) {
      this.$emit("show");

      if (this.autoZIndex) {
        ZIndexUtils.set(
          "modal",
          el,
          this.baseZIndex || this.$primevue.config.zIndex.modal
        );
      }
      this.focus();
      if (this.modal && !this.fullScreen) {
        this.containerRef(el);
        this.enableModality();
      }
    },
    onLeave() {
      this.$emit("hide");

      if (this.modal && !this.fullScreen) {
        this.disableModality();
      }
    },
    onAfterLeave(el) {
      if (this.autoZIndex) {
        ZIndexUtils.clear(el);
      }
    },
    focus() {
      // let focusable = DomHandler.findSingle(this.container, 'input,button');
      // if (focusable) {
      //     focusable.focus();
      // }
    },
    enableModality() {
      if (!this.mask) {
        this.mask = document.createElement("div");
        this.mask.setAttribute(
          "class",
          "p-sidebar-mask p-component-overlay p-component-overlay-enter"
        );
        this.mask.style.zIndex = String(
          parseInt(this.container.style.zIndex, 10) - 1
        );
        if (this.dismissable) {
          this.bindMaskClickListener();
        }
        document.body.appendChild(this.mask);
        DomHandler.addClass(document.body, "p-overflow-hidden");
      }
    },
    disableModality() {
      if (this.mask) {
        DomHandler.addClass(this.mask, "p-component-overlay-leave");
        this.mask.addEventListener("animationend", () => {
          this.destroyModal();
        });
      }
    },
    bindMaskClickListener() {
      if (!this.maskClickListener) {
        this.maskClickListener = () => {
          this.hide();
        };
        this.mask.addEventListener("click", this.maskClickListener);
      }
    },
    unbindMaskClickListener() {
      if (this.maskClickListener) {
        this.mask.removeEventListener("click", this.maskClickListener);
        this.maskClickListener = null;
      }
    },
    destroyModal() {
      if (this.mask) {
        this.unbindMaskClickListener();
        document.body.removeChild(this.mask);
        DomHandler.removeClass(document.body, "p-overflow-hidden");
        this.mask = null;
      }
    },
    containerRef(el) {
      this.container = el;
    },
  },
  computed: {
    containerClass() {
      return [
        "p-sidebar p-component p-sidebar-" + this.position,
        {
          "p-sidebar-active": this.visible,
          "p-input-filled": this.$primevue.config.inputStyle === "filled",
          "p-ripple-disabled": this.$primevue.config.ripple === false,
        },
      ];
    },
    fullScreen() {
      return this.position === "full";
    },
  },
  directives: {
    ripple: Ripple,
  },
};
</script>

<style>
.p-sidebar {
  position: fixed;
  transition: transform 0.3s;
}

.p-sidebar-content {
  height: 100%;
  position: relative;
  overflow-y: unset;
}

.p-sidebar-close {
  position: absolute;
  top: 0;
  right: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.p-sidebar-mask {
  background-color: transparent;
  transition-property: background-color;
}

.p-sidebar-mask.p-sidebar-mask-leave.p-component-overlay {
  background-color: transparent;
}

.p-sidebar-left {
  top: 0;
  left: 0;
  width: 20rem;
  height: 100%;
}

.p-sidebar-right {
  top: 0;
  right: 0;
  width: 20rem;
  height: 100%;
}

.p-sidebar-top {
  top: 0;
  left: 0;
  width: 100%;
  height: 10rem;
}

.p-sidebar-bottom {
  bottom: 0;
  left: 0;
  width: 100%;
  max-height: 95%;
  height: unset!important;
  /* height: 10rem; */
}

.p-sidebar-full {
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  -webkit-transition: none;
  transition: none;
}

.p-sidebar-relative {
  width: 100%;
  height: fit-content;
  min-height: 50%;
  bottom: 0;
  left: 0;
}

.p-sidebar-relative.p-sidebar-enter-from,
.p-sidebar-relative.p-sidebar-leave-to {
  transform: translateY(100%);
}

.p-sidebar-left.p-sidebar-enter-from,
.p-sidebar-left.p-sidebar-leave-to {
  transform: translateX(-100%);
}

.p-sidebar-right.p-sidebar-enter-from,
.p-sidebar-right.p-sidebar-leave-to {
  transform: translateX(100%);
}

.p-sidebar-top.p-sidebar-enter-from,
.p-sidebar-top.p-sidebar-leave-to {
  transform: translateY(-100%);
}

.p-sidebar-bottom.p-sidebar-enter-from,
.p-sidebar-bottom.p-sidebar-leave-to {
  transform: translateY(100%);
}

.p-sidebar-full.p-sidebar-enter-from,
.p-sidebar-full.p-sidebar-leave-to {
  opacity: 0;
}

.p-sidebar-full.p-sidebar-enter-active,
.p-sidebar-full.p-sidebar-leave-active {
  transition: opacity 400ms cubic-bezier(0.25, 0.8, 0.25, 1);
}

.p-sidebar-left.p-sidebar-sm,
.p-sidebar-right.p-sidebar-sm {
  width: 20rem;
}

.p-sidebar-left.p-sidebar-md,
.p-sidebar-right.p-sidebar-md {
  width: 40rem;
}

.p-sidebar-left.p-sidebar-lg,
.p-sidebar-right.p-sidebar-lg {
  width: 60rem;
}

/* .p-sidebar-top.p-sidebar-sm,
    .p-sidebar-bottom.p-sidebar-sm {
        height: 10rem;
    } */

.p-sidebar-top.p-sidebar-md,
.p-sidebar-bottom.p-sidebar-md {
  height: 20rem;
}

.p-sidebar-top.p-sidebar-lg,
.p-sidebar-bottom.p-sidebar-lg {
  height: 30rem;
}

@media screen and (max-width: 64em) {
  .p-sidebar-left.p-sidebar-lg,
  .p-sidebar-left.p-sidebar-md,
  .p-sidebar-right.p-sidebar-lg,
  .p-sidebar-right.p-sidebar-md {
    width: 20rem;
  }
}
</style>
