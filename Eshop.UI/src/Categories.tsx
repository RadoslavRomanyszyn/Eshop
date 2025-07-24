import { useState, useEffect } from "react";

export type CategoryType = {
    id: number;
    title: string;
    description: string;
};

type Props = {
    activeCategoryId: number
    onChange: (id: number) => void
};

const Categories = ({ activeCategoryId, onChange }: Props) => {
    const [categories, setCategories] = useState<CategoryType[]>([]);

    useEffect(() => {
        const loadCategories = async () => {
            const categoryResult = await (
                await fetch("https://localhost:7012/Category")
            ).json();

            setCategories(categoryResult);
        };

        loadCategories();
    }, []);

    return (
        <div>
            {categories.map((category) => {
                return (
                    <h2 
                        key={category.id}
                        onClick={() => onChange(category.id)}
                        style={{
                            cursor: "pointer",
                            textDecoration: category.id === activeCategoryId ? "underline" : "unset"
                        }}
                    >
                        {category.title}
                    </h2>
                );
            })}
        </div>
    );
};

export default Categories;
