import { useNavigate } from "react-router-dom";
import { logout } from "../../helpers/Logout";
import React, { useState } from "react";
import { Container, Nav, Navbar } from "react-bootstrap";

export const NavBar = () => {
   const navigate = useNavigate()
   const onLogout = () => {
      logout.logout(navigate);
   };

  return (
    <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark" sticky="top">
      <Container>
        <Navbar.Brand href="/">Time Well Spent</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="/activities">Activities</Nav.Link>
            <Nav.Link href="/myactivities">My Activities</Nav.Link>
            <Nav.Link href="/activityform">Make a Custom Activity</Nav.Link>
          </Nav>
          <Nav>
             <Nav.Link onClick={onLogout}>Logout</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}
