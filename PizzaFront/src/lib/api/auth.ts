import API from "../../api";
import {
  LoginUser,
  RegisterUser,
  SuccessfulLoginResult,
} from "../../types/authTypes";
import { saveToken } from "../authUtils";

async function login(userData: LoginUser) {
  const response = await API.post<SuccessfulLoginResult>("login", userData);

  if (response.status !== 200) {
    return false;
  }

  saveToken(response.data.accessToken);
  return true;
}

async function register(userData: RegisterUser) {
  const response = await API.post("register", userData);

  return response.status === 201;
}

export { login, register };
