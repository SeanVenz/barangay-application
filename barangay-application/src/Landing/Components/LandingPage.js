import React from 'react';
import Header from './Header';
import './LandingPage.css';
import Footer from './Footer';
import { Stack } from '@mui/system';

const LandingPage = () => {
    return (
        <div className="LandingPage">
            <Header/>
            <Stack direction="column" className="message-container" gap="1em">
                <h3 className="message1">Welcome to Barangay Registration System!</h3> 
                <p className="message2">
                    Join us today and experience a seamless registration process that puts you at the<br/>
                    forefront. By embracing technology, we're revolutionizing the way residents interact<br/>
                    with their Barangay. Together, let's build a stronger, more connected community.
                </p>
            </Stack>
            <Footer/>
        </div>
    );
};

export default LandingPage;