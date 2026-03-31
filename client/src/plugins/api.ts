import { useStorage } from '@vueuse/core'

import { AppRoutesEnum } from '~/config/app-routes'

export default defineNuxtPlugin((nuxtApp) => {
  const config = useRuntimeConfig()
  const accessToken = useStorage('accessToken', {}, localStorage)

  const api = $fetch.create({
    baseURL: config.public.apiBase,
    onRequest({ options }) {
      if (accessToken.value) {
        options.headers.set('Authorization', `${accessToken.value}`)
      }
    },
    async onResponseError({ response }) {
      if (response.status === 401) {
        const loginRoute = AppRoutesEnum.login
        await nuxtApp.runWithContext(() => navigateTo({ name: loginRoute }))
      }

      throw createError({ status: response.status, statusText: response._data?.message })
    },
  })

  return {
    provide: {
      api,
    },
  }
})
