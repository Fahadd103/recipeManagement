/* eslint-disable react/prop-types */
import { createContext, useState, useContext, useEffect } from 'react';
import {jwtDecode} from 'jwt-decode';


const AuthContext = createContext(null);

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
        try {
          const decodedToken = jwtDecode(token);
          setUser({ ...decodedToken, token });
        } catch (error) {
          console.error('Invalid token', error);
          localStorage.removeItem('token');
        }
    }
  }, []);

  const login = (token) => {
    if(token){
        const decodedToken = jwtDecode(token);
        localStorage.setItem('token', token);
        setUser({ ...decodedToken, token });
    }
  };

  return (
    <AuthContext.Provider value={{ user, login, setUser }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);

export default AuthProvider;
