import { createRouter, createWebHistory } from 'vue-router'
import AdminHome from '../pages/AdminHome.vue'

const routes = [{ path: '/', component: AdminHome }]

export const router = createRouter({
  history: createWebHistory('/admin/'),
  routes
})
