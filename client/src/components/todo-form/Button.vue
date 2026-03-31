<script setup lang="ts">
import type { TodoFormModalProps } from './Modal.vue'

import { mergeProps } from 'vue'

interface TodoFormButtonProps extends TodoFormModalProps {}

const props = defineProps<TodoFormButtonProps>()

const isEditing = computed(() => !!props?.id)
</script>

<template>
  <BButton variant="primary" v-bind="mergeProps($attrs)">
    {{ $t(isEditing ? 'editTodoTask' : 'addTodoTask') }}
  </BButton>
</template>
