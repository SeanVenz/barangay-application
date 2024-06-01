import React from 'react';
import { Box, Stack } from '@mui/system';
import './Header.css';
import logo from '../Images/Logo.png';
import { useNavigate } from 'react-router-dom';

const Header = () => {
    const navigate = useNavigate();

    const handleLoginClick = () => {
        navigate('/login');
    };

    const handleRegisterClick = () => {
        navigate('/register');
    };

    return (
        <Box className="Header-container">
            <Stack direction="row">
                <img
                    src={logo}
                    alt="Logo"
                    className="Logo"
                />
                <div className="Text-container"
                    style={{
                        fontFamily: "Montserrat Black, sans-serif"
                    }}
                >
                    <h5 className="Text1">Republic of the Philippines</h5> 
                    <h3 className="Text2">Barangay Registration System</h3>
                </div>
                <div className="buttons-container">
                    <button className="login-button" onClick={handleLoginClick}>LOGIN</button>
                    <button className="register-button" onClick={handleRegisterClick}>REGISTER</button>
                </div>
            </Stack>
        </Box>
    );
};

export default Header;