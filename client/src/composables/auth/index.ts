import type { UserAuthProps, UserLoginProps, UserSignupProps } from '~/models/user.models'

import { useMutation } from '@tanstack/vue-query'
import { useStorage } from '@vueuse/core'

import { apiRoutes } from '~/config/api-routes'

export function useLoginMutation() {
  const { $api } = useNuxtApp()
  const accessToken = useStorage('accessToken', {}, localStorage)
  const loginRoute = apiRoutes.login.post()

  return useMutation({
    mutationFn: async (payload: UserLoginProps) => {
      const response = await $api<UserAuthProps>(loginRoute, {
        method: 'POST',
        body: payload,
      })

      return response
    },

    onSuccess(data) {
      accessToken.value = `Bearer ${data.accessToken}`
    },
  })
}

export function useSignupMutation() {
  const { $api } = useNuxtApp()
  const accessToken = useStorage('accessToken', {}, localStorage)
  const signupRoute = apiRoutes.signup.post()

  return useMutation({
    mutationFn: async (payload: UserSignupProps) => {
      const response = await $api<UserAuthProps>(signupRoute, {
        method: 'POST',
        body: payload,
      })

      return response
    },

    onSuccess(data) {
      accessToken.value = `Bearer ${data.accessToken}`
    },
  })
}
