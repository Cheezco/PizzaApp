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

function getAxiosConfig() {
  return {
    headers: {
      Authorization: "Bearer " + getToken(),
    },
  };
}

export { getToken, saveToken, clearToken, getAxiosConfig };
