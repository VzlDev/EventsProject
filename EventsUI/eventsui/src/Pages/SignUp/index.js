import React, { useState } from 'react';
import axios from 'axios'; // For making HTTP requests to your backend API
import { Button, TextField } from '@mui/material';
import {api} from './Services/Api'

function SignUpPage() {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    fullName: '',
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
      console.log('form '); // Use JSON.stringify to log the object
      // Make a POST request to your backend API to authenticate the user
      const response = await api.post('/users/signUp', formData);
      // Handle successful login (e.g., save JWT token, redirect user, etc.)
      console.log('SignUp successful!', response.data);
    } catch (error) {
      // Handle login error (e.g., display error message to user)
      console.error('SignUp failed:', error.response.data.error);
    }
  };

  return (
    <div>
      <h2>SignUp</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Username:</label>
          <TextField type="text" name="username" value={formData.username} onChange={handleChange} />
        </div>
        <div>
          <label>Email:</label>
          <TextField type="text" name="email" value={formData.email} onChange={handleChange} />
        </div>
        <div>
          <label>Password:</label>
          <TextField type="password" name="password" value={formData.password} onChange={handleChange} />
        </div>
        <div>
          <label>Full Name:</label>
          <TextField type="text" name="fullName" value={formData.fullName} onChange={handleChange} />
        </div>
        <Button type="submit">Sign Up</Button>
      </form>
    </div>
  );
}

export default SignUpPage;
