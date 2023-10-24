const key = "jwt";

function getToken() {
  return sessionStorage.getItem(key) ?? "";
}

function saveToken(token: string) {
  sessionStorage.setItem(key, token);
}

function clearToken() {
  sessionStorage.removeItem(key);
}

export { getToken, saveToken, clearToken };
