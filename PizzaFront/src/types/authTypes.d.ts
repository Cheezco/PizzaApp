export interface LoginUser {
  username: string;
  password: string;
}

export interface RegisterUser {
  username: string;
  email: string;
  password: string;
}

export interface SuccessfulLoginResult {
  accessToken: string;
}

export interface User {
  id: string;
}

export interface JwtToken {
  aud: string;
  exp: number;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string[];
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  iss: string;
  jti: string;
  sub: string;
}
