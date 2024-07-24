import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ConfigurationPage from './pages/ConfigurationPage';

const AppRouter = () => {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/configuration" element={<ConfigurationPage />} />
        <Route exact path="/" element={<LoginPage />} />
      </Routes>
    </Router>
  );
};

export default AppRouter;
