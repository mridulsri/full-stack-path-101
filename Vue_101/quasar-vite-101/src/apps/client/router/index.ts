// src/client/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import ClientHome from '../pages/ClientHome.vue'

const routes = [{ path: '/', component: ClientHome }]

export const router = createRouter({
  history: createWebHistory('/client/'),
  routes
})
