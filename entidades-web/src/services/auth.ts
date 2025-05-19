import axios from "axios";

export async function login(username: string, password: string): Promise<string | null> {
  try {
    const res = await axios.post(
      "http://localhost:5009/api/Auth/login",
      { username, password },
      {
        headers: {
          "Content-Type": "application/json",
          "Accept": "*/*",
          "Access-Control-Allow-Origin": "*", // 🔧 won't help with browser CORS, but added as requested
        }
      }
    );
    return res.data.token;
  } catch (error) {
    console.error("Login failed:", error);
    return null;
  }
}
