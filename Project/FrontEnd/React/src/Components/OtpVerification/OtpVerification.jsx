import React, { useState } from 'react';
import './OtpVerfiication.css';
import { useNavigate } from 'react-router-dom';




function OtpVerification() {
    const navigate = useNavigate();
  const [otp, setOtp] = useState('');
  const email = localStorage.getItem('userEmail'); // retrieve email

  const handleSubmit = (e) => {
    e.preventDefault();

    console.log('Verifying OTP:', otp);
    // Add backend verification logic here
    alert(`OTP verified for ${email}`);
    console.log('OTP Verified:', otp);
  navigate('/change-password'); 
    
  };

  return (
    <div className="otp-container">
      <h2>OTP Verification</h2>
      <p>An OTP has been sent to <strong>{email}</strong></p>
      <form className="otp-form" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Enter OTP"
          value={otp}
          onChange={(e) => setOtp(e.target.value)}
          required
        />
        <button type="submit">Verify OTP</button>
      </form>
    </div>
  );
}

export default OtpVerification;
