export enum StatusFilterEnum {
  allTasks = 'allTasks',
  activeTasks = 'activeTasks',
  completedTasks = 'completedTasks',
}

export const useTodoTableControlsStore = defineStore('todo-table-controls-store', () => {
  const searchQuery = ref<string>('')
  const page = ref<number>(1)
  const pageLimit = ref(5)
  const status = ref(StatusFilterEnum.allTasks)

  return { status, searchQuery, page, pageLimit }
})
