<script setup lang="ts">
import { AppRoutesEnum } from '~/config/app-routes'

definePageMeta({
  name: AppRoutesEnum.login,
})
</script>

<template>
  <LoginForm />
</template>
