import type { CreateTodoTaskProps, PagedData, TodoTaskModel, UpdateTodoTaskProps } from '~/models/todo-task.models'
import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'

import { apiRoutes } from '~/config/api-routes'

export interface TodoTaskQuery {
  pageNumber?: number
  pageSize?: number
  search?: string
  status?: StatusFilterEnum
}

export function useTodoTaskListQuery(query?: MaybeRefOrGetter<TodoTaskQuery>) {
  const { $api } = useNuxtApp()
  const todoTaskListRoute = apiRoutes.taskList.get()

  const queryData = computed(() => toValue(query))

  return useQuery({
    queryKey: ['todo-task-list', query],
    queryFn: async () => {
      const response = await $api<PagedData<TodoTaskModel>>(todoTaskListRoute, {
        method: 'GET',
        query: queryData.value,
      })

      return response
    },
  })
}

export function useTodoTaskQuery(id?: MaybeRefOrGetter<TodoTaskModel['id'] | undefined>) {
  const { $api } = useNuxtApp()
  const entityId = computed(() => toValue(id))

  const todoTaskRoute = apiRoutes.task.get(entityId.value!)

  return useQuery({
    queryKey: ['todo-task', id],
    queryFn: async () => {
      const response = await $api(todoTaskRoute, {
        method: 'GET',
      })

      return response
    },
    select: todoTask => todoTask as TodoTaskModel,
    enabled: !!entityId.value,
  })
}

export function useCreateTodoTaskMutation() {
  const { $api } = useNuxtApp()
  const queryClient = useQueryClient()

  const route = apiRoutes.task.post()

  return useMutation({
    mutationFn: async (payload: CreateTodoTaskProps) => {
      const response = await $api<TodoTaskModel>(route, {
        method: 'POST',
        body: payload,
      })

      return response
    },

    onSuccess() {
      queryClient.invalidateQueries({ queryKey: ['todo-task-list'] })
    },
  })
}

export function useEditTodoTaskMutation(id: MaybeRefOrGetter<TodoTaskModel['id'] | undefined>) {
  const { $api } = useNuxtApp()
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: async (payload: UpdateTodoTaskProps) => {
      const entityId = toValue(id)
      if (!entityId)
        return

      const route = apiRoutes.task.put(entityId)

      const response = await $api<TodoTaskModel>(route, {
        method: 'PUT',
        body: payload,
      })

      return response
    },

    onSuccess() {
      queryClient.invalidateQueries({ queryKey: ['todo-task-list'] })
      queryClient.invalidateQueries({ queryKey: ['todo-task', id] })
    },
  })
}

export function useDeleteTodoTaskMutation(id: MaybeRefOrGetter<TodoTaskModel['id']>) {
  const { $api } = useNuxtApp()
  const queryClient = useQueryClient()

  const route = apiRoutes.task.delete(toValue(id))

  return useMutation({
    mutationFn: async () => {
      const response = await $api<number>(route, {
        method: 'DELETE',
      })

      return response
    },

    onSuccess() {
      queryClient.invalidateQueries({ queryKey: ['todo-task-list'] })
    },
  })
}
