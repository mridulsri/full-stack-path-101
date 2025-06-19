import { createApp } from 'vue'
import { Quasar } from 'quasar'
import App from './App.vue'
import { router } from './router'

createApp(App).use(Quasar, { plugins: {} }).use(router).mount('#q-app')
