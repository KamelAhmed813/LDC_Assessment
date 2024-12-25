# **RAG Bot Documentation**

## **Overview**

The Retrieval-Augmented Generation (RAG) Bot is designed to respond to user queries based on document content stored in the `\ChatBot\data\documents` directory. For this assessment, the bot is configured to process a sample company policy document. The bot consists of two main components:

1. **API Backend**: A .NET Core application that handles query and response data storage.  
2. **Python Model**: A FastAPI application that retrieves relevant document chunks and generates responses using an LLM.

---

## **Features**

* Processes any document added to the `\ChatBot\data\documents` directory.  
* Stores queries and responses in a Microsoft SQL database with relationships.  
* Uses FAISS for efficient similarity search and EleutherAI GPT-Neo-1.3B for response generation.  
* Includes endpoints for submitting user queries and receiving generated responses.

---

## **Prerequisites**

### **System Requirements**

* **Python**: Version `3.12.8`  
* **.NET SDK**: Version `8.0.404`  
* **Microsoft SQL Server**: Installed and accessible on the target machine.

### **Dependencies**

* Python dependencies are listed in `\ChatBot\requirements.txt`.

---

## **Setup and Installation**

### **Step 1: Clone the Repository**

`git clone [repository_url]`  
`cd [repository_name]`

### **Step 2: Python Environment Setup**

Navigate to the `\ChatBot` directory:

	`cd ChatBot`

1. Create and activate a Python virtual environment:  
   bash  
   `python -m venv venv`

`venv\Scripts\activate`

2. Install required Python packages:  
   bash  
   `pip install -r requirements.txt`

### **Step 3: Database Configuration**

Update the connection string in the .NET project (`\API\appsettings.json`) to match your Microsoft SQL Server instance:  
json  
`"ConnectionStrings": {`  
    `"DefaultConnection": "Server=[your_server_name];Database=[your_database_name];Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"`  
`}`

* Replace \[`your_server_name]`, \[`your_database_name]` with appropriate values.

Apply the database migrations:

`dotnet ef database update`

Create the database manually if migrations aren't included:

* Ensure the database schema matches the expected structure (`UserQuery`  
*  and `ChatBotResponse` tables).

---

### **Step 4: Running the Bot**

#### **Run the Python Model**

Navigate to the `\ChatBot` directory:

`cd ChatBot`

Start the FastAPI server:

`uvicorn main:app --port 8090`

**Run the API Backend**

Navigate to the `\API` directory:

`cd API`

Start the .NET API server:

`dotnet run`  
---

## **Usage**

### **Endpoint**

**POST `http://localhost:5264/API/query`**

#### **Request Body**

json

`{`  
  `"query": "[User question]"`  
`}`

#### **Response Example**

json

`{`  
  `"queryId": 1,`  
  `"response": "The notice period is 30 days from the date of the notice."`  
`}`

---

## **Pipeline Explanation**

1. **Document Preprocessing**:  
   * When the Python model initializes, it reads the content of `\ChatBot\data\documents`.  
   * The content is tokenized using `nltk.tokenize`, split into chunks of 500 characters, and stored in `\ChatBot\data\fileChunks`.  
2. **Vector Store Creation**:  
   * The chunks are embedded using `sentence-transformers/all-MiniLM-L6-v2`.  
   * A FAISS index is created and saved to `\ChatBot\data\vectorStore\vector_store.faiss`.  
3. **Query Handling**:  
   * A user sends a query to `http://localhost:5264/API/query`.  
   * The query is saved to the database and forwarded to the Python model.  
4. **Response Generation**:  
   * The Python model retrieves the most relevant document chunk using FAISS.  
   * The query and context are sent to `EleutherAI/gpt-neo-1.3B` to generate a response.  
   * The response is saved in the database and returned to the user.

---

## 

## **Directory Structure**

`project_root/`  
`├── ChatBot/`  
`│   ├── data/`  
`│   │   ├── documents/        # Input documents directory`  
`│   │   ├── fileChunks/       # Chunked document storage`  
`│   │   ├── vectorStore/      # FAISS vector store`  
`│   ├── main.py               # FastAPI app for Python model`  
`│   ├── requirements.txt      # Python dependencies`  
`├── API/`  
`│   ├── Program.cs            # Entry point for .NET API`  
`│   ├── appsettings.json      # Configuration file (e.g., connection strings)`  
`└── README.md                 # Documentation file`  
