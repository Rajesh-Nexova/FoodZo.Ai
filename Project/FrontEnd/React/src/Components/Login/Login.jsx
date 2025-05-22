import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Login.css';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!email || !password) {
      alert('Please enter email and password');
      return;
    }
    navigate('/dashboard');
  return;

    // try {
    //   const response = await axios.post('http://localhost:5000/api/UserLogin', {
    //     email: email,
    //     password: password
    //   });

    //   if (response.data.success) {
    //     alert('Login successful!');
        
    //     navigate('/dashboard');
    //   } else {
    //     alert(response.data.message || 'Login failed. Check your credentials.');
    //   }
    // } catch (error) {
    //   console.error('Login error:', error);
    //   alert('An error occurred during login.');
    // }
  };

  return (
    <div className="login-wrapper">
      {/* Left Side */}
      <div className="login-left">
        <div className="profile-pic mt-5">
          <img src="/images/foodZOAI_logo.jpg" alt="Profile" />
        </div>
        <div className="profile-banner mb-5">
          <img src="/images/foodZOAI_logo1.jpg" alt="Profile Banner" />
        </div>
      </div>

      {/* Right Side */}
      <div className="login-right">
        {/* Added image and H1 above login container */}
        

        <div className="login-container">
          <div className="top-banner text-center ">
          <img src="/images/foodZOAI_logo.jpg" alt="FoodZOAI Logo" style={{ width: '150px', height:'200px' }} />
         
        </div>
          <h2>Login</h2>
          <form className="login-form" onSubmit={handleSubmit}>
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
            <div className="forget-password">
              <Link to="/forget-password">Forget Password?</Link>
            </div>
            <button type="submit">Login</button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default Login;
