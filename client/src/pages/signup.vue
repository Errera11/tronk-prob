<script setup lang="ts">
import { AppRoutesEnum } from '~/config/app-routes'

definePageMeta({
  name: AppRoutesEnum.signup,
})
</script>

<template>
  <SignupForm />
</template>
