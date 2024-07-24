import React from 'react';
import './Header.css';

const Header = () => {
  return (
    <header className="header">
      <h1>Configuration Management</h1>
      <nav>
        <ul>
          <li><a href="/configuration">Configuration</a></li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;
