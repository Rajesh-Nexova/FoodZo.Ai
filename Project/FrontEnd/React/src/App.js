
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Login from './Components/Login/Login.jsx';
import ForgetPassword from './Components/ForgetPassword/ForgetPassword.jsx';
import OtpVerification from './Components/OtpVerification/OtpVerification.jsx';
import ChangePassword from './Components/ChangePassword/ChangePassword.jsx';
import Dashboard from './Components/Dashboard/Dashboard.jsx';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';

function App(){
  return (
    <div className='main'>
      <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" replace />}  />
        <Route path="/login" element={<Login/>}></Route>
        <Route path="/forget-password" element={<ForgetPassword />} />
        <Route path="/verify-otp" element={<OtpVerification />} />
        <Route path="/change-password" element={<ChangePassword />} /> 
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
      </BrowserRouter>
    </div>
  )
}


export default App;
