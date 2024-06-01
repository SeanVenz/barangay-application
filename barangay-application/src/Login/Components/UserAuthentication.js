import './UserAuthentication.css';
import axios from 'axios';
import React, { useState } from "react";
import { Stack, TextField, Button, IconButton, InputAdornment } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { NavLink, useNavigate } from 'react-router-dom';

function UserAuthentication() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const [error, setError] = useState("");

    const handleTogglePasswordVisibility = () => {
        setShowPassword((prevShowPassword) => !prevShowPassword);
    };

    const handleLogin = async () => {
        try {
          const response = await axios.post('http://localhost:7045/api/accounts/login', {
            email: email,
            password: password
          });

          if (response.status === 200) {
            console.log('Login successful');
            navigate('/barangays');
          } else {
            console.log('Invalid credentials');
            setError(response);
          }
          setError(response);
        } catch (error) {
          console.log('API error:', error.message);
          setError('Invalid email or password');
        }
      };

    const navigate = useNavigate();

    return (
        <div className="Module-bg">
            <h3 className="User-authentication">User Authentication</h3>
            <hr />
            <Stack direction = "column">
                <TextField
                    id="outlined-email"
                    label="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    sx={{ backgroundColor: "white", borderRadius: 3, marginRight: 3, width: 400 }}
                    required
                />
                <TextField
                    id="outlined-password"
                    label="Password"
                    type={showPassword ? "text" : "password"}
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    sx={{ backgroundColor: "white", borderRadius: 3, marginRight: 3, width: 400, marginTop: 5 }}
                    required
                    InputProps={{
                        endAdornment: (
                          <InputAdornment position="end">
                            <IconButton onClick={handleTogglePasswordVisibility} edge="end">
                              {showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                          </InputAdornment>
                        ),
                      }}
                />
                {error && <p style={{ color: "red" }}>{error}</p>}
                <div style={{ display: "flex", justifyContent: "center", marginTop: 25 }}>
                    <Button
                        size="large"
                        variant="contained"
                        style={{
                            background: "#BA0018",
                            width: 150,
                            borderRadius: 10,
                            color: "white",
                            fontFamily: "Montserrat Regular, sans-serif"
                        }}
                        onClick={handleLogin}>Login
                    </Button>
                </div>
                <div style={{ display: "flex", alignItems: "center", justifyContent: "center"}}>
                    <h5 className="Forgot-pw">No account yet?</h5>
                    <NavLink to="/register">
                    <Button
                        variant="text"
                        sx={{
                            fontFamily: "MontAserrat Black, sans-serif",
                            color: "#0128B4"}}>Click here to register
                    </Button>
                    </NavLink>
                </div>
            </Stack>
      </div>
        )
    }

export default UserAuthentication;