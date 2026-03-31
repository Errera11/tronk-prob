import type { UserModel } from './user.models'

export interface CreateTodoTaskProps {
  title: string
  description: string
  dueDate: string
}

export interface UpdateTodoTaskProps extends CreateTodoTaskProps {
  isCompleted: boolean
}

export interface TodoTaskModel {
  id: number
  title: string
  description: string
  dueDate: string
  isCompleted: boolean
  createdBy: UserModel
}

export interface PagedData<T> {
  data: T[]
  hasNextPage: boolean
  hasPreviousPage: boolean
  pageNumber: number
  pageSize: number
  totalPages: number
  totalRecords: number
}
