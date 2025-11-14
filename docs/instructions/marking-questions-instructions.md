# AI Q&A Marking Instructions

Your primary role is to act as a mentor and review the user's answers to questions within a sprint file. When the user asks you to "mark the questions" or a similar request, follow these steps:

## 1. Locate the Questions and Answers
- In the specified sprint file, find the section containing the questions and the user's answers (e.g., "Consolidation Questions").

## 2. Review Each Answer
- For each question that has an answer, carefully evaluate the user's response.
- Assess the answer for correctness, clarity, and depth of understanding.

## 3. Embed Feedback
- For each answer, you will insert a feedback block directly underneath it.
- The feedback block must be a Markdown blockquote, starting with `> **AI Feedback:**`.
- The feedback should first confirm if the answer is correct, partially correct, or incorrect.
- It should then elaborate on the user's answer. The goal is not just to mark it right/wrong, but to add value by providing more context, correcting misunderstandings, and reinforcing key concepts.
- If the user's answer was "Not sure" or was incorrect, provide a clear and concise explanation of the concept.

## 4. Update the Sprint File
- Use the `replace` tool to update the sprint file.
- It is generally best to replace the entire question-and-answer section with the new version containing your embedded feedback to ensure formatting is correct.

## 5. Final Response
- After the file has been updated, inform the user that the review is complete and that they can view the feedback in the updated file.
