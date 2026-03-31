<script setup lang="ts">
import type { CreateTodoTaskProps, TodoTaskModel, UpdateTodoTaskProps } from '~/models/todo-task.models'

import { mergeProps } from 'vue'

export interface TodoFormModalProps {
  id?: TodoTaskModel['id']
}

const props = defineProps<TodoFormModalProps>()

const isOpen = defineModel<boolean>({
  default: false,
})

const id = computed(() => props.id)
const isEditing = computed(() => !!props.id)

const { data, isFetching } = useTodoTaskQuery(id)

const { mutateAsync: createTask, isPending: isPendingCreateTask } = useCreateTodoTaskMutation()
const { mutateAsync: updateTask, isPending: isPendingUpdateTask } = useEditTodoTaskMutation(id)

const isPending = computed(() => isPendingCreateTask.value || isPendingUpdateTask.value)

const form = ref<CreateTodoTaskProps | UpdateTodoTaskProps>({
  title: '',
  description: '',
  dueDate: '',
  isCompleted: false,
})

watch(data, (data) => {
  if (data) {
    resetData(data)
  }
}, { immediate: true })

async function onSubmit() {
  if (isEditing.value) {
    await updateTask(form.value as UpdateTodoTaskProps)
  }
  else {
    await createTask(form.value)
  }

  closeModal()
}

function resetData({ title, description, dueDate, isCompleted }: TodoTaskModel) {
  form.value = {
    title,
    description,
    dueDate: dueDate.split('T')[0],
    isCompleted,
  }
}

function closeModal() {
  isOpen.value = false
}
</script>

<template>
  <ClientOnly>
    <BModal
      v-bind="mergeProps($attrs, props)"
      v-model="isOpen"
      :title="isEditing ? $t('editTask') : $t('createTask')"
      :cancel-title="$t('cancel')"
      @hidden="data && resetData(data)"
    >
      <BOverlay :show="isFetching">
        <BForm id="create-task-form" class="flex flex-col gap-2" @submit.prevent="onSubmit">
          <BFormInput
            id="input-1"
            v-model="form.title"
            :placeholder="$t('taskTitle')"
            name="title"
            type="text"
            required
          />

          <BFormInput
            id="input-2"
            v-model="form.description"
            :placeholder="$t('taskDescription')"
            name="description"
            type="text"
            required
          />

          <BFormInput
            id="input-3"
            v-model="form.dueDate"
            :placeholder="$t('dueDate')"
            type="date"
            name="dueDate"
            required
          />

          <BFormCheckbox
            v-if="isEditing"
            v-model="(form as UpdateTodoTaskProps).isCompleted"
            :placeholder="$t('dueDate')"
            name="dueDate"
          >
            {{ $t('taskCompleted') }}
          </BFormCheckbox>
        </BForm>
      </BOverlay>

      <template #ok>
        <BButton type="submit" variant="primary" :loading="isPending" form="create-task-form">
          {{ $t(isEditing ? 'editTodoTask' : 'createTask') }}
        </BButton>
      </template>
    </BModal>
  </ClientOnly>
</template>
