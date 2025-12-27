import {
  createContext,
  useContext,
  useState,
  useEffect,
  useRef,
  useLayoutEffect,
} from "react";
import {
  fetchProfile,
  loginRequest,
  logoutRequest,
} from "../services/user-service";
import { tokenStorage } from "@/shared/storage/token-storage";
import { QueryClient, useQuery, useQueryClient } from "@tanstack/react-query";

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const trackuser = useRef(false);

  const login = async (username, password) => {
    try {
      const res = await loginRequest(username, password);
      if (res.status === 200) {
        tokenStorage.set(res.data.token);
        console.log(`login-token ${res.data.token}`);
        await loadUserProfile();
        return { success: true };
      }
      return { success: false, message: "Invalid input" };
    } catch (error) {
      if (error.response?.status === 400) {
        return { success: false, message: "Invalid input" };
      }
      return { success: false, message: "Server error" };
    }
  };

  const logout = async () => {
    try {
      await logoutRequest();
      tokenStorage.clear();
      localStorage.removeItem("profile");
      window.location.href = "/login";
    } catch (error) {
      tokenStorage.clear();
      throw error;
    }
  };

  const signup = async (username, email, password) => {
    try {
      var { status } = await registerNewUser(username, email, password);
      //redirect
      if (status === 400) {
        //
      }

      if (status === 201) {
        window.location.href = "/login";
      }
    } catch (error) {
      console.log(error);
    }
  };

  const loadUserProfile = async () => {
    try {
      const { data } = await fetchProfile();
      setUser(data);
      localStorage.setItem("profile", JSON.stringify(data));
      console.log(`user-login ${JSON.stringify(data)}`);
    } catch {
      tokenStorage.clear();
      setUser(null);
    }
  };

  useLayoutEffect(() => {
    const initAuth = async () => {
      if (trackuser.current) return;
      trackuser.current = true;

      try {
        const { data } = await fetchProfile();
        setUser(data);
        //localStorage.setItem("profile", JSON.stringify(data));
        console.log(`user-login ${JSON.stringify(data)}`);
      } catch {
        tokenStorage.clear();
        setUser(null);
      }
    };
    initAuth();
  }, []);

  return (
    <AuthContext.Provider value={{ user, setUser, login, logout, signup }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) {
    throw new Error("outside scope !");
  }
  return ctx;
};
