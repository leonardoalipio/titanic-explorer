import { useState } from "react"

const initialFormSearch = {
    Survived: null,
    Class: null,
    Sex: null,
    Age: null,
    Fare: null,
}

const PassengerSearch = ({setPassengers}) => {
    const [form, setForm] = useState(initialFormSearch)

    const change = ((e) => setForm({
        ...form,
        [e.target.name]: parseInt(e.target.value),
    }))

    const searchPassengers = async (e) => {
        e.preventDefault();
        fetch("http://127.0.0.1:5012/search-passengers", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(form),
        })
        .then((response) => response.json())
        .then((data) => {
            setPassengers(data);
        });
    }

    return (
        <form className="blog-post" onSubmit={searchPassengers}>
            <div className="row">
                <div className="form-group col">
                    <label htmlFor="age" className="form-label">Survived?</label>
                    <select name="survived" className="form-control"
                        onChange={(e) => setForm({...form, [e.target.name]: parseInt(e.target.value)})}>
                        <option value=""></option>
                        <option value="1">Survived</option>
                        <option value="0">Perished</option>
                    </select>
                </div>
                <div className="form-group col">
                    <label htmlFor="age" className="form-label">PClas</label>
                    <select name="class" className="form-control"
                        onChange={(e) => setForm({...form, [e.target.name]: parseInt(e.target.value)})}>
                        <option value=""></option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                </div>

                <div className="form-group col">
                    <label htmlFor="sex" className="form-label">Sex</label>
                    <select name="sex" className="form-control"
                        onChange={(e) => setForm({...form, [e.target.name]: parseInt(e.target.value)})}>
                        <option value=""></option>
                        <option value="0">Male</option>
                        <option value="1">Female</option>
                    </select>
                </div>

                <div className="form-group col">
                    <label htmlFor="age" className="form-label">Age</label>
                    <input name="age" className="form-control" placeholder="Age" type="text" 
                    value={form.Age} 
                    onChange={change} />
                </div>

                <div className="form-group col">
                    <label htmlFor="fare" className="form-label">Maximum Fare</label>
                    <input name="fare" className="form-control" placeholder="fare" type="text" 
                    value={form.Fare} 
                    onChange={change} />
                </div>
            </div>

            <button type="submit" className="btn btn-primary mt-3">Search</button>
        </form>
    )
}

export default PassengerSearch