import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AddActivity, AddUta, FetchActivities, FetchActivityById, FetchCategories, FetchMoods, FetchUserToActivities, FetchUsers, FetchWeeks } from "../ApiManager";
import { Button, Col, Form, InputGroup, ModalDialog, Row } from "react-bootstrap";

export const CustomActivityForm = () => {
   const localTimeUser = localStorage.getItem("capstone_user");
   const timeUserObject = JSON.parse(localTimeUser);
   const [moods, setMoods] = useState([]);
   const [categories, setCategories] = useState([]);
   const [weeks, setWeeks] = useState([]);
   const [activities, setActivities] = useState([]);
   const [users, setUsers] = useState([]);
   const [currentUser, updateCurrentUser] = useState({
      id: 0,
      name: "",
      email: "",
      firebaseUid: ""
   });
   const [uta, updateUta] = useState({
      userId: 0,
      activityId: 0,
      categoryId: 0,
      hoursSpent: 0,
      moodId: 0,
      createdBy : timeUserObject.uid,
      dateOccured : new Date(),
      description: "",
      weekId: 0
   });
   const [customActivity, updateActivity] = useState({
      name: "",
      image: ""
   });
   const navigate = useNavigate();

   useEffect(() => {
      const fetchMoods = async () => {
         const moodsFromApi = await FetchMoods();
         setMoods(moodsFromApi);
      };
      fetchMoods()
      console.log(moods)
   }, []);

   
   useEffect(() => {
      const fetchUsersFromApi = async () => {
         const usersFromApi = await FetchUsers();
         setUsers(usersFromApi);
      };
      fetchUsersFromApi()
   }, []);

   useEffect(() => {
      const makeCurrentUserWork = () => {
         if (users.length > 0) {
            console.log(users)
            const currentUserId = users.find(u => u.email === timeUserObject.email);
            console.log(currentUserId)
            updateCurrentUser(currentUserId)
         }
      }
      makeCurrentUserWork();
      console.log(currentUser)
   }, [users])

   useEffect(() => {
      const fetchCategories = async () => {
         const categoriesFromApi = await FetchCategories();
         setCategories(categoriesFromApi);
      };
      fetchCategories();
   }, []);

   useEffect(() => {
      const fetchWeeks = async () => {
         const weeksFromApi = await FetchWeeks();
         setWeeks(weeksFromApi);
      }
      fetchWeeks();
   }, []);

   const fetchActivities = async () => {
      const activitiesFromApi = await FetchActivities();
      setActivities(activitiesFromApi);
   }

   const handleSaveButtonClick = (event) => {
      event.preventDefault();

      AddActivity(customActivity);
      fetchActivities();
      const newActivity = activities.slice();
      uta.activityId = newActivity.id;
      AddUta(uta);
      navigate("/myactivities");
   };

   return (
      <Form>
      <Row className="mb-3">
        <Form.Group as={Col} controlId="formGridName">
          <Form.Label>Name of Activity</Form.Label>
          <Form.Control 
          placeholder="Enter name of your Activity" 
          value={customActivity.name}
          onChange={
            (event) => {
                const copy = {...customActivity}
                copy.name = event.target.value
                updateActivity(copy)
                console.log(copy)
            }
          } />
        </Form.Group>
  
        <Form.Group as={Col} controlId="formGridImage">
          <Form.Label>Activity Picture</Form.Label>
          <Form.Control 
          placeholder="URL here"
          value={customActivity.image}
          onChange={
            (event) => {
              const copy = {...customActivity}
              copy.image = event.target.value
              updateActivity(copy)
            }
          } />
          <Form.Text>
          Please upload the URL of the image you want to use
          </Form.Text>
        </Form.Group>
      </Row>

      <InputGroup>
         <InputGroup.Text>Description</InputGroup.Text>
         <Form.Control
            as="textarea"
            placeholder="Please write a short Description of the Activity..."
            id="descriptionBox"
            aria-label="Description"
            value={uta.description}
            onChange={
               (event) => {
                  const copy  = {...uta};
                  copy.description = event.target.value;
                  updateUta(copy);
               }
            }
         />
      </InputGroup>

      <Form.Group controlId="formGridMood">
        <Form.Label>Mood</Form.Label>
        <Form.Select
          onChange={
            (event) => {
              const copy = {...uta}
              const setmoodId = moods.find(mood => mood.name === event.target.value)
              copy.moodId = setmoodId.id         
              updateUta(copy)
              console.log(uta)
              console.log(moods)
            }
          }
        >
          <option>Choose the Mood you felt at this Activity...</option>
          {moods.map(
            (mood) => {
              return (
                <option key={`mood--${mood.id}`}>{mood.name}</option>
              )
            }
          )}
        </Form.Select>
      </Form.Group>

      <Form.Group controlId="formGridCategory">
        <Form.Label>Category</Form.Label>
        <Form.Select
          onChange={
            (event) => {
              const copy = {...uta}
              const setcategoryId = categories.find(category => category.name === event.target.value)
              copy.categoryId = setcategoryId.id         
              updateUta(copy)
              console.log(uta)
            }
          }
        >
          <option>Choose the category you feel this Activity falls under...</option>
          {categories.map(
            (category) => {
              return (
                <option key={`category--${category.id}`}>{category.name}</option>
              )
            }
          )}
        </Form.Select>
      </Form.Group>

      <Form.Group controlId="formGridWeek">
        <Form.Label>Week Start Date</Form.Label>
        <Form.Select
          onChange={
            (event) => {
              const copy = {...uta}
              const setweekId = weeks.find(week => week.startDate === event.target.value)
              copy.weekId = setweekId.id         
              updateUta(copy)

            }
          }
        >
          <option>Choose the Week on which this Activity happened...</option>
          {weeks.map(
            (week) => {
              return (
                <option key={`week--${week.id}`}>{week.startDate}</option>
              )
            }
          )}
        </Form.Select>
      </Form.Group>
  
      <Button variant="primary" type="submit" onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}>
        Submit
      </Button>
    </Form>
   )
}