<script setup lang="ts">
import { StatusFilterEnum } from '~/stores/todo-table-controls-store'

const options = [
  { text: $t(StatusFilterEnum.allTasks), value: StatusFilterEnum.allTasks },
  { text: $t(StatusFilterEnum.activeTasks), value: StatusFilterEnum.activeTasks },
  { text: $t(StatusFilterEnum.completedTasks), value: StatusFilterEnum.completedTasks },
]

const store = useTodoTableControlsStore()
const { searchQuery, status } = storeToRefs(store)
</script>

<template>
  <div class="flex gap-5 items-center w-full px-4 py-3 bg-default">
    <BFormRadioGroup
      v-model="status"
      class="flex-2"
      :options="options"
      button-variant="outline-primary"
      name="radios-btn-outline"
      buttons
    />

    <BInput
      v-model="searchQuery"
      class="flex-1"
      :placeholder="$t('searchTasks')"
    >
      {{ $t('addTodoTask') }}
    </BInput>
  </div>
</template>
