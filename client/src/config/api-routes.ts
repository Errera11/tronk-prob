import type { TodoTaskModel } from '~/models/todo-task.models'

export const apiRoutes = {
  taskList: {
    get: () => 'todotasks',
  },
  task: {
    get: (id: TodoTaskModel['id']) => `todotasks/${id}`,
    post: () => 'todotasks',
    put: (id: TodoTaskModel['id']) => `todotasks/${id}`,
    delete: (id: TodoTaskModel['id']) => `todotasks/${id}`,

  },
  login: {
    post: () => 'auth/login',
  },
  signup: {
    post: () => 'auth/signup',
  },
}
