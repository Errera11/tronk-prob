export interface UserModel {
  id: number
  email: string
  createdAt: string
}

export interface UserLoginProps {
  email: string
  password: string
}

export interface UserSignupProps extends UserLoginProps {}

export interface UserAuthProps {
  accessToken: string
  user: UserModel
}
