import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Register.css";
import "../../assets/styles/global.css";
import { register } from "../../api";

function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("Submitting register with username:", username, "email:", email, "password:", password); // Debugging
    try {
      const response = await register({ username, email, password });
      console.log("Register successful, response:", response.data);
      navigate("/login");
    } catch (error) {
      setError("User creation failed");
      console.error("Error registering user", error);
    }
  };

  return (
    <div className="container">
      <div className="formDiv">
        <h1>Register</h1>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="Username"
            required
          />
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="Email"
            required
          />
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Password"
            required
          />
          <button type="submit">Register</button>
        </form>
        {error && <p className="error">{error}</p>}

      </div>
    </div>
  );
}

export default Register;
