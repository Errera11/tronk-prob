<script setup lang="ts">
import type { TodoFormModalProps } from './Modal.vue'

interface TodoFormProps extends TodoFormModalProps {}

const props = defineProps<TodoFormProps>()

const isModal = defineModel<boolean>({
  default: false,
})

function openModal() {
  isModal.value = true
}
</script>

<template>
  <div>
    <TodoFormButton v-bind="props" @click="openModal" />
    <LazyTodoFormModal v-if="isModal" v-bind="props" v-model="isModal" />
  </div>
</template>
