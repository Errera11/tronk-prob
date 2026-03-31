<script setup lang="ts">
import { AppRoutesEnum } from '~/config/app-routes'

interface LoginFormProps {
  email: string
  password: string
  rememberMe: boolean
}

const { mutateAsync, isPending, isError, error } = useSignupMutation()

const form = reactive<LoginFormProps>({
  email: '',
  password: '',
  rememberMe: false,
})

function onSubmit() {
  mutateAsync(form)
}
function onReset() {}
</script>

<template>
  <div class="m-auto p-6 bg-white w-[50%] text-center">
    <h3>{{ $t('signup') }}</h3>

    <h2>To-Do List</h2>

    <p>{{ $t('manageTasksMffectively') }}</p>

    <BForm class="flex flex-col gap-1" @submit.prevent="onSubmit" @reset="onReset">
      <BFormGroup
        id="input-group-1"
        label-for="input-1"
      >
        <BFormInput
          id="input-1"
          v-model="form.email"
          type="email"
          :placeholder="$t('email')"
          required
        />
      </BFormGroup>

      <BFormGroup
        id="input-group-1"
        label-for="input-1"
      >
        <BFormInput
          id="input-1"
          v-model="form.password"
          type="password"
          :placeholder="$t('password')"
          required
        />
      </BFormGroup>

      <NuxtLink
        :to="{
          name: AppRoutesEnum.login,
        }"
      >
        {{ $t('alreadyHaveAnAccount') }}
      </NuxtLink>

      <BButton type="submit" variant="primary" :loading="isPending">
        {{ $t('login') }}
      </BButton>

      <span v-if="isError">{{ error?.message }}</span>
    </BForm>
  </div>
</template>
