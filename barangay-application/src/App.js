import './App.css';
import MapScreen from './Barangay/Components/MapScreen';
import RegisterUpdateDeleteBarangay from './Barangay/Components/RegisterUpdateDeleteBarangay';
import LoginPage from './Login/Components/Login.js';
import Registration from './AdminRegistration/Components/Registration.js';
import Family from './FamilyRegistration/Components/Family.js';
import FamilyMember from './RegisterFamilyMember/Components/FamilyMember.js';
import { Routes, Route } from 'react-router-dom';
import Landing from './Landing/Components/LandingPage';

function App() {
  return (
    <div>
      <Routes>
        <Route path="/" element={<Landing/>}/>
        <Route path="login" element={<LoginPage/>}/>
        <Route path="register" element={<Registration/>}/>
        <Route path="barangays" element={<MapScreen/>}/>
        <Route path="barangays/manage" element={<RegisterUpdateDeleteBarangay/>}/>
        <Route path="barangays/:name/families" element={<Family/>}/>
        <Route path="barangays/:name/families/:id/members" element={<FamilyMember/>}/>
      </Routes>
    </div>
  );
}

export default App;