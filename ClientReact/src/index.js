import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Route, Routes, Outlet } from "react-router-dom";
import globalStore from './redux/globalStore'
import { Provider } from "react-redux";
import Header from './components/common/Header';
import Footer from './components/common/Footer';
import MainPage from './components/pages/MainPage';
import PostPage from './components/pages/PostPage';
import RequestCreateNewGroupPage from './components/pages/RequestCreateNewGroupPage';
import AboutPage from './components/pages/AboutPage';
import LoginPage from './components/pages/LoginPage';
import RegisterLicencePage from './components/pages/RegisterLicencePage';
import RegisterPage from './components/pages/RegisterPage';
import GroupPage from './components/pages/GroupPage';
import AddNewPostPage from './components/pages/AddNewPostPage';
import UserPage from './components/pages/UserPage';
import SearchPage from './components/pages/SearchPage';
import AdminPage from './components/pages/AdminPage';
import { AuthLayout } from './layouts/AuthLayout';
import reportWebVitals from './reportWebVitals';

import axios from 'axios';
import config from './config.js'

import './i18n';
import './index.css';

axios.defaults.baseURL = config.ServerUrl;

function BasicLayout() {
  return (
    <>
      <Header />
      <div className="root_border">
          <div className="root_content">
            <Outlet />
          </div>
      </div>
      <Footer />
    </>
  )
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={globalStore}>
    <React.StrictMode>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={ <BasicLayout />}>
            <Route index element={<MainPage />} />
            <Route path="login" element={<LoginPage />} />
            <Route path="registerLicence" element={<RegisterLicencePage />} />
            <Route path="register" element={<RegisterPage />} />
            <Route path="about" element={<AboutPage />} />
            <Route path="group/:id" element={<GroupPage />} />
            <Route path="group/:groupId/post/:postId" element={<PostPage />} />
            <Route element={<AuthLayout />}>
              <Route path="requestCreateNewGroup" element={<RequestCreateNewGroupPage />} />
              <Route path="addNewPost/:id" element={<AddNewPostPage />} />
              <Route path="user/:id" element={<UserPage />} />
              <Route path="search/:text" element={<SearchPage />} />
              <Route path="admin" element={<AdminPage />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </React.StrictMode>
  </Provider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
