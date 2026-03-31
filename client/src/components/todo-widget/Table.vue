<script setup lang="ts">
import type { TableFieldRaw, TableItem } from 'bootstrap-vue-next'

import type { TodoTaskModel } from '~/models/todo-task.models'
import { formatDate } from '~/utils/format-date'

const store = useTodoTableControlsStore()
const { page, pageLimit, searchQuery, status } = storeToRefs(store)

const debouncedSearch = refDebounced(searchQuery)
const query = computed<TodoTaskQuery>(() => ({
  pageNumber: page.value,
  pageSize: pageLimit.value,
  search: debouncedSearch.value,
  status: status.value,
}))

const { data, isFetching } = useTodoTaskListQuery(query)

const items = computed<TableItem<TodoTaskModel>[]>(() =>
  (data.value?.data ?? []).map(d => ({
    id: d.id,
    title: d.title,
    dueDate: d.dueDate,
    createdBy: d.createdBy,
    isCompleted: d.isCompleted,
    description: d.description,
  })),
)
const totalRows = computed(() => data.value?.totalRecords ?? 1)

const fields: Exclude<TableFieldRaw<TodoTaskModel>, string>[] = [
  {
    key: 'isCompleted',
    label: ' ',
  },
  {
    key: 'title',
    label: $t('title'),
  },
  {
    key: 'date',
    label: $t('date'),
  },
  {
    key: 'author',
    label: $t('author'),
  },
  {
    key: 'actions',
    label: ' ',
  },
]
</script>

<template>
  <div class="w-full h-full bg-white">
    <BOverlay
      class="min-h-0"
      :show="isFetching"
      rounded="sm"
    >
      <BTable
        sticky-header
        :items
        :fields
        show-empty
      >
        <template #cell(isCompleted)="row">
          <BFormCheckbox v-model="row.item.isCompleted as boolean" disabled />
        </template>

        <template #cell(author)="row">
          {{ row.item.createdBy.email }}
        </template>

        <template #cell(date)="row">
          {{ formatDate(row.item.dueDate) }}
        </template>

        <template #cell(actions)="row">
          <div class="flex gap-1">
            <TodoForm :id="row.item.id" />
            <TodoWidgetDeleteButton :id="row.item.id" />
          </div>
        </template>
      </BTable>
    </BOverlay>

    <BFoot>
      <BPagination
        v-model="page"
        class="px-2"
        :total-rows
        :per-page="pageLimit"
        align="fill"
      />
    </BFoot>
  </div>
</template>
