from fastapi.testclient import TestClient
from main import app

client = TestClient(app)

def test_ask_query_success():
    # Input data for the test
    data = {
        "query": "What is the notice period?",
        "queryId": 1
    }

    # Send POST request to the /process endpoint
    response = client.post("/ask", json=data)

    # Assertions
    assert response.status_code == 200  # Expecting success
    response_data = response.json()
    assert response_data["queryId"] == 1
    assert len(response_data["response"]) > 0

'''
def test_process_query_empty_query():
    """
    Test the /process endpoint with an empty query.
    """
    # Input data with an empty query
    data = {
        "query": "",
        "queryId": 2
    }

    # Send POST request to the /process endpoint
    response = client.post("/process", json=data)

    # Assertions
    assert response.status_code == 422  # Unprocessable Entity due to validation error

def test_process_query_invalid_queryId():
    """
    Test the /process endpoint with an invalid queryId.
    """
    # Input data with a negative queryId
    data = {
        "query": "What is the capital of France?",
        "queryId": -1
    }

    # Send POST request to the /process endpoint
    response = client.post("/process", json=data)

    # Assertions
    assert response.status_code == 422  # Unprocessable Entity due to validation error
'''