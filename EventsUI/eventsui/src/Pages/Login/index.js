import React, { useState } from 'react';
import axios from 'axios'; // For making HTTP requests to your backend API
import { Button, TextField } from '@mui/material';
import { Link } from 'react-router-dom';

function LoginPage() {
  const [formData, setFormData] = useState({
    username: '',
    password: ''
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Make a POST request to your backend API to authenticate the user
      const response = await axios.post('https://localhost:7174/users/login', formData);
      // Handle successful login (e.g., save JWT token, redirect user, etc.)
      console.log('Login successful!', response.data);
    } catch (error) {
      // Handle login error (e.g., display error message to user)
      console.error('Login failed:', error.response.data.error);
    }
  }; 

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Username:</label>
          <TextField type="text" name="username" value={formData.username} onChange={handleChange} />
        </div>
        <div>
          <label>Password:</label>
          <TextField type="password" name="password" value={formData.password} onChange={handleChange} />
        </div>
        <Button type="submit">Login</Button>
        <Link to="/signup"> Sign Up</Link>
      </form>
    </div>
  );
}

export default LoginPage;
