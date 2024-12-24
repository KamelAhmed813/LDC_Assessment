import os

#openai.api_key = os.getenv("OPENAI_API_KEY")

def generate_response(query, context):
    prompt = f"Answer the following question based on the context:\n\nContext: {context}\n\nQuestion: {query}"
    return f"The Answer for your query\n{query}\nis in the following Context:\n{context}"
    '''
    response = openai.Completion.create(
        engine="text-davinci-003",
        prompt=prompt,
        max_tokens=300
    )
    return response.choices[0].text.strip()
    '''
