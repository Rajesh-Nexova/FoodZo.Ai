import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './ForgetPassword.css';

function ForgetPassword() {
  const [email, setEmail] = useState('');
  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();

    console.log('Sending OTP to:', email);
    // You can add backend logic here to send OTP

    // Store email in localStorage or pass via state (for demo, using localStorage)
    localStorage.setItem('userEmail', email);

    // Navigate to OTP verification page
    navigate('/verify-otp');
  };

  return (
    <div className="forget-container">
      <h2>Forget Password</h2>
      <form className="forget-form" onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="Enter your registered email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <button type="submit">Send OTP</button>
      </form>
    </div>
  );
}

export default ForgetPassword;
