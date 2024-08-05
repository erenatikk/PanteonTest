import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";
import "../../assets/styles/global.css";
import { login } from "../../api";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await login({ username, password });
      console.log("Login successful, response:", response.data); 
      localStorage.setItem("token", response.data.token);
      navigate("/configuration");
    } catch (error) {
      setError("Invalid username or password");
      console.error("Error logging in", error);
    }
  };

  return (
    <div className="container">
      <div className="formDiv">
        <h1>Login</h1>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="Username"
            required
          />
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Password"
            required
          />
          {error && <p className="error">{error}</p>}
          <button type="submit">Login</button>
        </form>
        <p>Don't have an account? <Link to="/register">Register here</Link></p>

      </div>
    </div>
  );
}

export default Login;
