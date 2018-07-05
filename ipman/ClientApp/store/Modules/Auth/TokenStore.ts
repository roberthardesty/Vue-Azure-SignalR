
const emailTokenId = "email_token"
const refreshTokenId = "refresh_token"

export namespace JWT {
  export function fetch(): {email_token: string, refresh_token: string} {
    return {email_token: localStorage.getItem(emailTokenId), refresh_token: localStorage.getItem(refreshTokenId)}
  }

  export function set(tokens: {email_token: string, refresh_token: string}) {
    localStorage.setItem(emailTokenId, tokens.email_token);
    localStorage.setItem(refreshTokenId, tokens.refresh_token);
  }

  export function clear() {
    localStorage.removeItem(emailTokenId);
    localStorage.removeItem(refreshTokenId);
  }
}
