<script setup lang="ts">
import type { TodoTaskModel } from '~/models/todo-task.models'

interface TodoWidgetDeleteButtonProps {
  id: TodoTaskModel['id']
}

const props = defineProps<TodoWidgetDeleteButtonProps>()

const { mutateAsync: deleteTask } = useDeleteTodoTaskMutation(props.id)

function handlaeDeleteTask() {
  deleteTask()
}
</script>

<template>
  <BButton @click="handlaeDeleteTask">
    {{ $t('deleteTask') }}
  </BButton>
</template>
