import { useNavigate, useParams } from "react-router-dom";
import { AddUta, FetchActivities, FetchActivityById, FetchCategories, FetchMoods, FetchUserToActivities, FetchUsers, FetchWeeks} from "../ApiManager";
import { useEffect, useState } from "react";
import { Button, Col, Form, InputGroup, Row } from "react-bootstrap";

export const ActivityDetails = () => {
   const {activityDetailsId} = useParams();
   const localTimeUser = localStorage.getItem("capstone_user");
   const timeUserObject = JSON.parse(localTimeUser);
   const [activityToEdit, setActivityToEdit] = useState({});
   const [moodDefault, setMoodDefault] = useState({});
   const [categoryDefault, setCategoryDefault] = useState({});
   const [weekDefault, setWeekDefault] = useState({});
   const [moods, setMoods] = useState([]);
   const [categories, setCategories] = useState([]);
   const [weeks, setWeeks] = useState([]);
   const [users, setUsers] = useState([]);
   const [utas, setUtas] = useState([]);
   const [correctUta, setCorrectUta] = useState({});
   const [currentUser, updateCurrentUser] = useState({
      id: 0,
      name: "",
      email: "",
      firebaseUid: ""
   });
   const navigate = useNavigate();

   useEffect(() => {
      const getCustomActivity = async () => {
         const activityToEditFromApi = await FetchActivityById(activityDetailsId);
         setActivityToEdit(activityToEditFromApi);
      }
      getCustomActivity();
   }, [activityDetailsId]);

   useEffect(() => {
      const makeCurrentUserWork = () => {
         if (users.length > 0) {
            const currentUserId = users.find(u => u.email === timeUserObject.email);
            updateCurrentUser(currentUserId)
         }
      }
      makeCurrentUserWork();
   }, [users]);

   useEffect(() => {
      const getUtasFromAPI = async () => {
         const utasFromApi = await FetchUserToActivities();
         setUtas(utasFromApi);
      }
      getUtasFromAPI(); 
   }, [])

   // useEffect(() => {
   //    if(utas.length > 0 && utas.ac) {
   //       const filteredUta = utas.filter(uta => uta.activityId === parseInt(activityDetailsId));
   //       setCorrectUta(filteredUta[0]);
   //    }
   // }, 
   // [activityDetailsId, utas]);

   useEffect(() => {
      const fetchMoods = async () => {
         const moodsFromApi = await FetchMoods();
         setMoods(moodsFromApi);
      };
      fetchMoods()
   }, []);

   useEffect(() => {
      if(moods.length > 0 && correctUta.id !== undefined) {
         const moodForForm = moods.filter(mood => mood.id === correctUta.moodId)
         setMoodDefault(moodForForm[0]);
      }
   }, [moods, correctUta])

   useEffect(() => {
      const fetchUsersFromApi = async () => {
         const usersFromApi = await FetchUsers();
         setUsers(usersFromApi);
      };
      fetchUsersFromApi()
   }, []);

   useEffect(() => {
      const fetchCategories = async () => {
         const categoriesFromApi = await FetchCategories();
         setCategories(categoriesFromApi);
      };
      fetchCategories();
   }, []);

   useEffect(() => {
      if(categories.length > 0 && correctUta.id !== undefined) {
         const categoryForForm = categories.filter(category => category.id === correctUta.categoryId)
         setCategoryDefault(categoryForForm[0]);
      }
   }, [categories, correctUta])

   useEffect(() => {
      const fetchWeeks = async () => {
         const weeksFromApi = await FetchWeeks();
         setWeeks(weeksFromApi);
      }
      fetchWeeks();
   }, []);

   useEffect(() => {
      if(weeks.length > 0 && correctUta.id !== undefined) {
         const weekForForm = weeks.filter(week => week.id === correctUta.weekId)
         setWeekDefault(weekForForm[0]);
      }
   }, [weeks, correctUta]);

   useEffect(() => {
      const getActivitiesFromApi = async () => {
         const ActivitesFromApi = await FetchActivities();
         setActivityToEdit(ActivitesFromApi);
      }
      getActivitiesFromApi();
   }, []);

   const handleSaveButtonClick = async (event) => {
      event.preventDefault();

      correctUta.activityId = activityToEdit.id;
      correctUta.userId = currentUser.id;
      AddUta(correctUta);
      navigate("/myactivities");
   };

   return (
      <Form>
      <Row className="mb-3">
        <Form.Group as={Col} controlId="formGridName">
          <Form.Label>Name of Activity</Form.Label>
          <Form.Control 
          placeholder="Enter name of your Activity"
          disabled
          readonly 
          value={activityToEdit.name}
          onChange={
            (event) => {
                const copy = {...activityToEdit}
                copy.name = event.target.value
                setActivityToEdit(copy)
            }
          } />
        </Form.Group>
  
        <Form.Group as={Col} controlId="formGridImage">
          <Form.Label>Activity Picture</Form.Label>
          <Form.Control 
          placeholder="URL here"
          value={activityToEdit.image}
          disabled
          readonly
          onChange={
            (event) => {
              const copy = {...activityToEdit}
              copy.image = event.target.value
              setActivityToEdit(copy)
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
            disabled
            readonly
            value={correctUta.description}
            onChange={
               (event) => {
                  const copy  = {...correctUta};
                  copy.description = event.target.value;
                  setCorrectUta(copy);
               }
            }
         />
      </InputGroup>

      <Form.Group controlId="formGridMood">
        <Form.Label>Mood</Form.Label>
        <Form.Select
        value={moodDefault.name}
        disabled
        readonly
          onChange={
            (event) => {
              const copy = {...correctUta}
              const setmoodId = moods.find(mood => mood.name === event.target.value)
              copy.moodId = setmoodId.id         
              setCorrectUta(copy)
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
        value={categoryDefault.name}
        disabled
        readonly
          onChange={
            (event) => {
              const copy = {...correctUta}
              const setcategoryId = categories.find(category => category.name === event.target.value)
              copy.categoryId = setcategoryId.id         
              setCorrectUta(copy)
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
        value={weekDefault.startDate}
        disabled
        readonly
          onChange={
            (event) => {
              const copy = {...correctUta}
              const setweekId = weeks.find(week => week.startDate === event.target.value)
              copy.weekId = setweekId.id         
              setCorrectUta(copy)

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
        Save Activity to your Own Page
      </Button>
    </Form>
   )
}