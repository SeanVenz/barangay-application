import React from "react";
import "./Login.css";
import Header from "./Header";
import UserAuthentication from "./UserAuthentication";
import phMap from "../Images/phMap.png";

function Login() {
  return (
    <div className="Login-page" 
      style={{
        userSelect: 'none',
        WebkitUserSelect: 'none',
        MozUserSelect: 'none',
        msUserSelect: 'none',
  }}>
      <Header/>
      <h1 className="Login-header">
        GOVERNMENT OFFICIALS ACCESS MODULE
      </h1>
      <UserAuthentication />
      <img className="login-ph-map" alt={phMap} src={phMap} loading="lazy" draggable="false" 
        style={{
          width: "300px",
          height: "470px",
        }}
      />
    </div>
  );
}

export default Login;