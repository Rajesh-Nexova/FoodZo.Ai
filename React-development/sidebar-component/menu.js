import { Anchor, Headphones, Home } from "react-feather";

export const MENUITEMS = [
  {
    title: "Dashboard",
    icon: Home,
    type: "sub",
    badgeType: "primary",
    active: false,
    children: [
      { path: "/dashboard/default", title: "Default", type: "link" },
      { path: "/dashboard/ecommerce", title: "E-Commerce", type: "link" },
      { path: "/dashboard/university", title: "University", type: "link" },
      { path: "/dashboard/server", title: "Server", type: "link" },
      { path: "/dashboard/project", title: "Project", type: "link" },
    ],
  },
  {
    title: "Support Ticket",
    icon: Anchor,
    type: "link",
    path: "/support-ticket/supportTicket",
    active: false,
  },
  {
    path: "http://support.pixelstrap.com/help-center",
    title: "Raise Support",
    icon: Headphones,
    type: "exteral_link",
    active: false,
  },
];
