import { createI18n } from 'vue-i18n/index'
import en from './en.js'
import id from './id.js'

const i18n = createI18n({
    legacy: false,
    locale: 'en',
    globalInjection: true,
    messages:{
        en: en,
        id: id
    },
    
})

export default i18n;