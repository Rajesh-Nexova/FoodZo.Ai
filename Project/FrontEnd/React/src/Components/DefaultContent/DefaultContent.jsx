import React from 'react';
import { FaUsers, FaDollarSign, FaChartBar, FaCogs } from 'react-icons/fa';

const DefaultContent = () => {
  return (
    <>
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
    </>
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

export default DefaultContent;
