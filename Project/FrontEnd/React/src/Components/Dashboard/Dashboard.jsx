import React from 'react';
import { FaChartBar, FaUsers, FaDollarSign, FaCogs } from 'react-icons/fa';

const Dashboard = () => {
  return (
    <div className="d-flex min-vh-100 bg-light">
      {/* Sidebar */}
      <div className="bg-white border-end p-4" style={{ width: '200px' }}>
        <h4 className="mb-4">FoodZOAI</h4>
        <ul className="nav flex-column">
          <li className="nav-item">
            <a href="#" className="nav-link text-dark">
              <FaChartBar className="me-2" />
              Overview
            </a>
          </li>
          <li className="nav-item">
            <a href="#" className="nav-link text-dark">
              <FaUsers className="me-2" />
              Users
            </a>
          </li>
          <li className="nav-item">
            <a href="#" className="nav-link text-dark">
              <FaDollarSign className="me-2" />
              Sales
            </a>
          </li>
          <li className="nav-item">
            <a href="#" className="nav-link text-dark">
              <FaCogs className="me-2" />
              Settings
            </a>
          </li>
        </ul>
      </div>

      {/* Main Content */}
      <div className="flex-grow-1 d-flex flex-column">
        {/* Topbar */}
        <nav className="navbar navbar-light bg-white shadow-sm px-4">
          <div className="container-fluid">
            <span className="navbar-brand mb-0 h1">Dashboard</span>
            <div className="d-flex align-items-center">
              <span className="me-2">Santhosh</span>
              <img
                src="https://i.pravatar.cc/40"
                alt="avatar"
                className="rounded-circle"
                width="40"
                height="40"
              />
            </div>
          </div>
        </nav>

        {/* Dashboard Content */}
        <div className="container-fluid p-4">
          {/* Widgets */}
          <div className="row g-4 mb-4">
            <DashboardWidget icon={<FaUsers />} title="Users" value="1,250" />
            <DashboardWidget icon={<FaDollarSign />} title="Sales" value="$53,400" />
            <DashboardWidget icon={<FaChartBar />} title="Revenue" value="$12,800" />
            <DashboardWidget icon={<FaCogs />} title="Settings" value="3 Updated" />
          </div>

          {/* Content Area */}
          <div className="card">
            <div className="card-header fw-semibold">Recent Activity</div>
            <div className="card-body">
              <p className="text-muted">You can add tables, charts, or logs here.</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

const DashboardWidget = ({ icon, title, value }) => {
  return (
    <div className="col-md-6 col-lg-3">
      <div className="card shadow-sm h-100">
        <div className="card-body d-flex align-items-center">
          <div className="fs-3 text-primary me-3">{icon}</div>
          <div>
            <div className="text-muted small">{title}</div>
            <div className="fw-bold fs-5">{value}</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
