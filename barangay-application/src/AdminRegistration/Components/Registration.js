import React, { useState } from 'react';
import { Box, Stack } from '@mui/system';
import { Button, Dialog, DialogContent, DialogTitle, IconButton, InputAdornment, TextField, Typography } from '@mui/material';
import './Registration.css';
import Header from './Header';
import axios from 'axios';
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { useNavigate, Link } from 'react-router-dom';

const Registration = () => {
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        emailAddress: '',
        phoneNumber: '',
        governmentIdNumber: '',
        password: '',
        confirmPassword: '',
    });

    const [errors, setErrors] = useState({});

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (formData.password !== formData.confirmPassword) {
            setErrors({ confirmPassword: "Passwords do not match" });
            return;
        }

        const requiredFields = ['firstName', 'lastName', 'emailAddress', 'phoneNumber', 'governmentIdNumber', 'password', 'confirmPassword'];

        const emptyFields = requiredFields.filter(field => !formData[field]);

        if (emptyFields.length > 0) {
            const errorMessages = emptyFields.reduce((acc, field) => {
                return {
                    ...acc,
                    [field]: `${field} is required`,
                };
            }, {});

            setErrors(errorMessages);
            return;
        }

        try {
            const { confirmPassword, ...requestData } = formData;

            const response = await axios.post('http://localhost:7045/api/accounts', requestData);
            console.log(response.data);
            setErrorMessage(response);
            setOpen(true);
        } catch (error) {
            if (error.response) {
                console.log(error.response.data);
                setErrorMessage("Please review account information.");
            } else {
                console.log(error.message);
                setErrorMessage(error.message);
                setErrorMessage("Please review account information.");
            }
            setOpenErrorDialog(true);
        }
    };

    const [showPassword, setShowPassword] = useState(false);

    const handleTogglePasswordVisibility = () => {
        setShowPassword((prevShowPassword) => !prevShowPassword);
    };

    const [showConfirmPassword, setShowConfirmPassword] = useState(false);

    const handleToggleConfirmPasswordVisibility = () => {
        setShowConfirmPassword((prevShowPassword) => !prevShowPassword);
    };

    const [open, setOpen] = useState(false);

    const handleClose = () => {
        setOpen(false);
    };

    const [errorMessage, setErrorMessage] = useState('');

    const [openErrorDialog, setOpenErrorDialog] = useState(false);

    const handleCloseErrorDialog = () => {
        setOpenErrorDialog(false);
    };

    const navigate = useNavigate();

    const handleContinueClick = () => {
        navigate('/barangays');
    };

    return (
        <div className="Registration">
            <Header/>
            <Stack direction="row" justifyContent="space-evenly" alignItems="flex-start" marginTop="10px">
                <Box className="Registration-form">
                    <Stack direction="column" alignItems="left">
                        <Typography variant="h5" fontFamily="Montserrat Black, sans-serif" fontWeight={900} color="#BA0018">
                            Create Account
                        </Typography>
                        <Typography variant="h6" fontFamily="Montserrat Black, sans-serif" fontWeight="bold" color="#BA0018" marginTop="10px">
                            Personal Information
                        </Typography>
                        <Stack direction="row" justifyContent="left" alignItems="flex-start" marginTop="15px">
                            <TextField
                                required
                                id="firstname-required"
                                label="First Name"
                                value={formData.firstName}
                                onChange={(event) => setFormData({ ...formData, firstName: event.target.value })}
                                error={!!errors.firstName}
                                helperText={errors.firstName ? errors.firstName : ' '}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    marginRight: '50px',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                            />
                            <TextField
                                required
                                id="lastname-required"
                                label="Last Name"
                                value={formData.lastName}
                                onChange={(event) => setFormData({ ...formData, lastName: event.target.value })}
                                error={!!errors.lastName}
                                helperText={errors.lastName ? errors.lastName : ' '}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                            />
                        </Stack>
                        <Stack direction="row" justifyContent="left" alignItems="flex-start" marginTop="25px">
                            <TextField
                                required
                                id="email-required"
                                label="Email Address"
                                value={formData.emailAddress}
                                onChange={(event) => setFormData({ ...formData, emailAddress: event.target.value })}
                                error={!!errors.emailAddress}
                                helperText={errors.emailAddress ? errors.emailAddress : ' '}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    marginRight: '50px',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                            />
                            <TextField
                                required
                                id="phone-required"
                                label="Phone Number"
                                value={formData.phoneNumber}
                                onChange={(event) => setFormData({ ...formData, phoneNumber: event.target.value })}
                                error={!!errors.phoneNumber}
                                helperText={errors.phoneNumber ? errors.phoneNumber : ' '}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                            />
                        </Stack>
                        <Stack direction="row" justifyContent="left" alignItems="flex-start" marginTop="25px">
                            <TextField
                                required
                                id="govid-required"
                                label="Government ID Number"
                                value={formData.governmentIdNumber}
                                onChange={(event) => setFormData({ ...formData, governmentIdNumber: event.target.value })}
                                error={!!errors.governmentIdNumber}
                                helperText={errors.governmentIdNumber ? errors.governmentIdNumber : ' '}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    marginRight: '50px',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                            />
                        </Stack>
                        <div className="Line"></div>
                        <Typography variant="h6" fontFamily="Montserrat Black, sans-serif" fontWeight="bold" color="#BA0018">
                            Setup Your Password
                        </Typography>
                        <Typography variant="caption" fontFamily="Montserrat Black, sans-serif" color="#BA0018">
                            Password must have at least 8 characters, an uppercase, a number, and a special character
                        </Typography>
                        <Stack direction="row" justifyContent="left" alignItems="flex-start" marginTop="15px">
                            <TextField
                                required
                                id="password-required"
                                label="Password"
                                value={formData.password}
                                onChange={(event) => setFormData({ ...formData, password: event.target.value })}
                                error={!!errors.confirmPassword}
                                helperText={errors.confirmPassword}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    marginRight: '50px',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton onClick={handleTogglePasswordVisibility} edge="end">
                                                {showPassword ? <VisibilityOff /> : <Visibility />}
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                                type={showPassword ? "text" : "password"}
                            />
                            <TextField
                                required
                                id="confirmpassword-required"
                                label="Confirm Password"
                                value={formData.confirmPassword}
                                onChange={(event) => setFormData({ ...formData, confirmPassword: event.target.value })}
                                error={!!errors.confirmPassword} // Check if confirmPassword error exists
                                helperText={errors.confirmPassword}
                                sx={{
                                    backgroundColor: '#FFFFFF',
                                    width: '250px',
                                    height: '55px',
                                    borderRadius: '10px',
                                    '& .MuiOutlinedInput-root': {
                                    borderRadius: '10px',
                                    },
                                }}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton onClick={handleToggleConfirmPasswordVisibility} edge="end">
                                                {showConfirmPassword ? <VisibilityOff /> : <Visibility />}
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                                type={showConfirmPassword ? "text" : "password"}
                            />
                        </Stack>
                        <Stack direction="row" justifyContent="left" alignItems="flex-start">
                            <div className="Text">
                                <span>Already have an account?</span>
                                <Link to="/login" className="Login-text">Login</Link>
                            </div>
                            <Button
                                variant="contained"
                                onClick={handleSubmit}
                                sx={{
                                    backgroundColor: '#BA0018',
                                    width: '250px',
                                    height: '55px',
                                    fontWeight: 'bold',
                                    borderRadius: '10px',
                                    fontSize: '20px',
                                    fontFamily: 'Montserrat Regular, sans-serif',
                                    marginTop: '30px',
                                }}
                            >
                                Submit
                            </Button>
                        </Stack>
                    </Stack>
                </Box>
            </Stack>
            <Dialog
                open={open}
                onClose={handleClose}
                BackdropProps={{
                    onClick: (event) => event.stopPropagation(),
                }}
                PaperProps={{
                    onClick: (event) => event.stopPropagation(),
                }}
                disableBackdropClick={true}
            >
                <DialogTitle>Registration Successful</DialogTitle>
                <DialogContent>
                    <Typography variant="body1">Welcome! Please click "Continue" to get started.</Typography>
                </DialogContent>
                <Button variant="contained" onClick={handleContinueClick}>
                    Continue
                </Button>
            </Dialog>
            <Dialog
                open={openErrorDialog}
                onClose={handleCloseErrorDialog}
                disableBackdropClick={true}
            >
                <DialogTitle>Error</DialogTitle>
                <DialogContent>
                <Typography variant="body1">{errorMessage}</Typography>
                </DialogContent>
                <Button variant="contained" onClick={handleCloseErrorDialog}>
                    OK
                </Button>
            </Dialog>
        </div>
    );
};

export default Registration;