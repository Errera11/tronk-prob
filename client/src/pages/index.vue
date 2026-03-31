<script setup lang="ts">
import { AppRoutesEnum } from '~/config/app-routes'

definePageMeta({
  name: AppRoutesEnum.home,
})
</script>

<template>
  <div class="bg-red flex items-center justify-center w-full h-full">
    <div class="w-[80%]">
      <TodoWidget />
    </div>
  </div>
</template>
